import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { ProductResponse } from '../models/products-response.model';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private apiUrl = 'https://localhost:7221/api/product';

  constructor(private http: HttpClient) { }

  getProducts(page: number, productsPerPage: number): Observable<ProductResponse> {
    // return this.http.get<ProductResponse>(this.apiUrl);
    console.log("Page", page);
    console.log("productsPerPage", productsPerPage);
    const skip = (page - 1) * productsPerPage;
    const url = `${this.apiUrl}?productsPerPage=${productsPerPage}&page=${page}`;

    return this.http.get<ProductResponse>(url);
  }

  searchProducts(query: string): Observable<ProductResponse> {
    const url = `${this.apiUrl}/search?q=${query}`;
    return this.http.get<ProductResponse>(url);
  }  
}