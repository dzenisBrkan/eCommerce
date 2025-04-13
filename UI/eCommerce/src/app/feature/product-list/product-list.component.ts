import { Component, OnInit, TemplateRef } from '@angular/core';
import { ProductService } from '../services/products.service';
import { Product, ProductResponse } from '../models/products-response.model';
import { Router } from '@angular/router';
import { ProductDetails } from '../models/products-details.model';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';


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

  modalProduct: Product | null = null;

  selectedSort: string = 'title-asc';

  currentImageIndex: number = 0;

  constructor(
     private productService: ProductService,
     private router: Router,
     private modalService: NgbModal
    ) { }

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts(sortBy: string = "title", orderBy: string = "asc") {
    this.productService.getProducts(this.currentPage, this.productsPerPage, sortBy, orderBy).subscribe({
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

   sortProducts(): void {
    switch (this.selectedSort) {
      case 'title-asc':
        this.loadProducts("title", "asc");
        break;
      case 'title-desc':
        this.loadProducts("title", "desc");
        break;
      case 'price-asc':
        this.loadProducts("price", "asc");
        break;
      case 'price-desc':
        this.loadProducts("price", "desc");
        break;
      default:
        break;
    }
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
  
  openProductModal(productId: number, productModal: TemplateRef<any>): void {
    // Fetch product details based on the productId
    this.productService.getProductById(productId).subscribe({
      next: (product) => {
        this.modalProduct = product; // Store the product details to display in the modal
        this.currentImageIndex = 0; 
        // Open the modal and handle the result (closed or dismissed)
        const modalRef = this.modalService.open(productModal, { size: 'lg' });
  
        modalRef.result.then(
          (result) => {
            console.log(`Modal closed with: ${result}`);
          },
          (reason) => {
            console.log(`Modal dismissed with: ${reason}`);
          }
        );
      },
      error: (err) => {
        console.error('Error fetching product:', err);
      }
    });
  }

 // Move to the previous image
 previousImage(): void {
  if (this.currentImageIndex > 0) {
    this.currentImageIndex--;
  }
}

// Move to the next image
nextImage(): void {
  if (this.modalProduct != null && this.currentImageIndex < this.modalProduct?.images.length - 1) {
    this.currentImageIndex++;
  }
}

  closeModal(): void { 
    const modalRef = this.modalService.dismissAll(); 
  }

  imageLenght(): number{
    return this.modalProduct?.images.length ?? 0;
  }

  getImage(curentIndeximg: number): string {
    return this.modalProduct?.images[curentIndeximg] ?? "";
  }
  
  isLoggedIn(): boolean {
    return !!localStorage.getItem('access_token');
  }

  showToast = false;
toastMessage = '';

addToBucket(product: any): void {
  let bucket = JSON.parse(localStorage.getItem('bucket') || '[]');
  const index = bucket.findIndex((item: any) => item.product.id === product.id);

  if (index >= 0) {
    bucket[index].quantity++;
  } else {
    bucket.push({ product, quantity: 1 });
  }

  localStorage.setItem('bucket', JSON.stringify(bucket));

  // Show toast
  this.toastMessage = `${product.title} added to your bucket!`;
  this.showToast = true;

  // Auto-hide after 3 seconds
  setTimeout(() => {
    this.showToast = false;
  }, 3000);
}

  
}
