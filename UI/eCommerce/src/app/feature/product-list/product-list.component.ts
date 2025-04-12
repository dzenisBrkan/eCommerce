import { Component, OnInit } from '@angular/core';
import { ProductOne } from '../models/products.model';
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
  productsPerPage: number = 10; // Number of products per page

  constructor(private productService: ProductService) { }

  ngOnInit(): void {
    this.loadProducts();
  }

  // Fetch products from the API
  loadProducts() {
    this.productService.getProducts().subscribe({
      next: (data: ProductResponse) => {
        this.products = data.products;
        this.totalProducts = data.total;
        this.loading = false;
      },
      error: (err) => {
        console.error('Error fetching products:', err);
        this.loading = false;
      }
    });
  }

  // // Calculate the products to show on the current page
  // currentProducts(): Product[] {
  //   const startIndex = (this.currentPage - 1) * this.productsPerPage;
  //   const endIndex = startIndex + this.productsPerPage;
  //   return this.products.slice(startIndex, endIndex);
  // }

  // // Handle page change (Next/Previous)
  // onPageChange(page: number): void {
  //   if (page >= 1 && page <= this.totalPages()) {
  //     this.currentPage = page;
  //   }
  // }

  // // Calculate total number of pages
  // totalPages(): number {
  //   return Math.ceil(this.totalProducts / this.productsPerPage);
  // }
}
