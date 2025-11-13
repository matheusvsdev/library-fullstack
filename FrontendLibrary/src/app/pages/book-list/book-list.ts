import { Component, OnInit } from '@angular/core';
import { ICategoryResponse } from '../../interfaces/category-response';
import { IBookRequest } from '../../interfaces/book-request';
import { IBookResponse } from '../../interfaces/book-response';
import { BookService } from '../../services/book';
import { CategoryService } from '../../services/category';

import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';

import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { DialogModule } from 'primeng/dialog';
import { MultiSelectModule } from 'primeng/multiselect';
import { ToastModule } from 'primeng/toast';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ConfirmationService, MessageService } from 'primeng/api';
import { FormsModule } from '@angular/forms';

import { CardModule } from 'primeng/card';
import { ToolbarModule } from 'primeng/toolbar';
import { IconFieldModule } from 'primeng/iconfield';
import { InputIconModule } from 'primeng/inputicon';

@Component({
  selector: 'app-book-list',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    FormsModule,
    TableModule,
    ButtonModule,
    InputTextModule,
    DialogModule,
    MultiSelectModule,
    ToastModule,
    ConfirmDialogModule,
    CardModule,
    ToolbarModule,
    IconFieldModule,
    InputIconModule,
  ],
  providers: [MessageService, ConfirmationService],
  templateUrl: './book-list.html',
  styleUrl: './book-list.css',
})
export class BookList implements OnInit {
  books: IBookResponse[] = [];
  categories: ICategoryResponse[] = [];

  bookForm!: FormGroup;
  isDialogVisible = false;
  isEditMode = false;
  currentBookId: string | number | null = null;
  filterTitle = '';

  constructor(
    private bookService: BookService,
    private categoryService: CategoryService,
    private fb: FormBuilder,
    private messageService: MessageService,
    private confirmationService: ConfirmationService
  ) {}

  ngOnInit(): void {
    this.bookForm = this.fb.group({
      title: ['', Validators.required],
      author: ['', Validators.required],
      categoryIds: [[], Validators.required],
    });

    this.loadBooks();
    this.loadCategories();
  }

  loadBooks(): void {
    this.bookService.getBooks(this.filterTitle).subscribe((data) => {
      this.books = data;
    });
  }

  loadCategories(): void {
    this.categoryService.getCategories().subscribe((data) => {
      this.categories = data;
    });
  }

  onFilter(): void {
    this.loadBooks();
  }

  openNew(): void {
    this.isEditMode = false;
    this.bookForm.reset();
    this.isDialogVisible = true;
  }

  editBook(book: IBookResponse): void {
    this.isEditMode = true;
    this.currentBookId = book.id;

    this.bookForm.setValue({
      title: book.title,
      author: book.author,
      categoryIds: book.categories.map((c) => c.id),
    });

    this.isDialogVisible = true;
  }

  saveBook(): void {
    if (this.bookForm.invalid) {
      this.bookForm.markAllAsTouched(); // Mostra erros se houver
      return;
    }

    const bookRequest: IBookRequest = this.bookForm.value;

    const operation = this.isEditMode
      ? this.bookService.updateBook(this.currentBookId!, bookRequest)
      : this.bookService.createBook(bookRequest);

    const successMessage = this.isEditMode ? 'Livro atualizado!' : 'Livro criado!';

    operation.subscribe({
      next: () => {
        this.messageService.add({
          severity: 'success',
          summary: 'Sucesso',
          detail: successMessage,
        });
        this.loadBooks();
        this.isDialogVisible = false;
      },
      error: (err) => {
        // Mostra o erro vindo do GlobalExceptionHandler do .NET
        this.messageService.add({
          severity: 'error',
          summary: 'Erro',
          detail: err.error?.detail || 'Erro ao salvar.',
        });
      },
    });
  }

  deleteBook(book: IBookResponse): void {
    this.confirmationService.confirm({
      message: `Tem certeza que deseja excluir "${book.title}"?`,
      header: 'Confirmação',
      icon: 'pi pi-exclamation-triangle',

      acceptLabel: 'Sim',
      rejectLabel: 'Não',
      accept: () => {
        this.bookService.deleteBook(book.id).subscribe({
          next: () => {
            this.messageService.add({
              severity: 'success',
              summary: 'Sucesso',
              detail: 'Livro excluído!',
            });
            this.loadBooks();
          },
          error: (err) => {
            // Ex: "Não é possível excluir..."
            this.messageService.add({
              severity: 'error',
              summary: 'Erro',
              detail: err.error?.detail || 'Erro ao excluir.',
            });
          },
        });
      },
    });
  }

  formatCategories(categories: ICategoryResponse[]): string {
    if (!categories || categories.length === 0) return 'N/A';
    return categories.map((c) => c.name).join(', ');
  }
}
