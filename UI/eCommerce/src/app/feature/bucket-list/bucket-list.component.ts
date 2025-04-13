import { Component } from '@angular/core';
import { Product } from '../models/products-response.model';

@Component({
  selector: 'app-bucket-list',
  standalone: false,
  templateUrl: './bucket-list.component.html',
  styleUrl: './bucket-list.component.css'
})
export class BucketListComponent {
  cartItems: { product: Product; quantity: number }[] = [];

  constructor() {
    const savedCart = localStorage.getItem('bucket');
    this.cartItems = savedCart ? JSON.parse(savedCart) : [];
  }

  increaseQuantity(index: number): void {
    this.cartItems[index].quantity++;
    this.saveCart();
  }

  decreaseQuantity(index: number): void {
    if (this.cartItems[index].quantity > 1) {
      this.cartItems[index].quantity--;
      this.saveCart();
    }
  }

  removeItem(index: number): void {
    this.cartItems.splice(index, 1);
    this.saveCart();
  }

  getTotal(): number {
    return this.cartItems.reduce(
      (total, item) => total + item.product.price * item.quantity,
      0
    );
  }

  saveCart(): void {
    localStorage.setItem('bucket', JSON.stringify(this.cartItems));
  }
}
