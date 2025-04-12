import { Component, OnInit } from '@angular/core';
import { ProductService } from '../services/products.service';
import { Product, ProductResponse } from '../models/products-response.model';

@Component({
  selector: 'app-product-list',
  standalone: false,
  templateUrl: './product-list.component.html',
  styleUrl: './product-list.component.css'
})
export class ProductListComponent implements OnInit {
  products: Product[] = [];
  loading = true;
  totalProducts: number = 0;
  currentPage: number = 1;
  productsPerPage: number = 10;
  totalPages: number = 0;

  searchQuery: string = '';

  constructor(private productService: ProductService) { }

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts() {
    this.productService.getProducts(this.currentPage, this.productsPerPage).subscribe({
      next: (data: ProductResponse) => {
        this.products = data.products;
        this.totalProducts = data.total;
        this.totalPages = Math.ceil(this.totalProducts / this.productsPerPage);
        this.loading = false;
      },
      error: (err) => {
        console.error('Error fetching products:', err);
        this.loading = false;
      }
    });
  }

  currentProducts(): Product[] {
    const startIndex = (this.currentPage - 1) * this.productsPerPage;
    const endIndex = startIndex + this.productsPerPage;
    return this.products.slice(startIndex, endIndex);
  }

  onPageChange(page: number): void {
    if (page >= 1 && page <= this.totalPages) {
      this.currentPage = page;
      this.loadProducts();
    }
  }

  onSearch(): void {
    this.loading = true;
    this.productService.searchProducts(this.searchQuery).subscribe({
      next: (data: ProductResponse) => {
        this.products = data.products;
        this.totalProducts = data.total;
        this.loading = false;
      },
      error: (err) => {
        console.error('Search error:', err);
        this.loading = false;
      }
    });
} 

}
