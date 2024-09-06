import { Inject, Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from '../../environments/environment';

export interface ProductDto {
  id: number;
  quantity: number;
  price: number;
  title: string;
  category: string;
  description: string;
}

@Injectable({
  providedIn: 'root'
})
export class ProductsService {
  private apiUrl = `${environment.API_BASE_URL}/api/products`

  constructor(private http: HttpClient,@Inject('BASE_URL') baseUrl: string) {
    this.apiUrl = `${baseUrl}api/products`
  }

  // GET: api/products/{id}
  getProductById(id: number): Observable<ProductDto> {
    return this.http.get<ProductDto>(`${this.apiUrl}/${id}`);
  }

  // GET: api/products?page=1&pageCount=100
  getProducts(page: number = 1, pageCount: number = 100): Observable<ProductDto[]> {
    const params = new HttpParams()
      .set('page', page.toString())
      .set('pageCount', pageCount.toString());
    return this.http.get<ProductDto[]>(this.apiUrl, { params });
  }

  // POST: api/products
  createProduct(product: ProductDto): Observable<ProductDto> {
    return this.http.post<ProductDto>(this.apiUrl, product, {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    });
  }

  // PUT: api/products
  editProduct(product: ProductDto): Observable<void> {
    return this.http.put<void>(this.apiUrl, product, {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    });
  }

  // DELETE: api/products/{id}
  deleteProduct(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  // POST: api/products/upload
  uploadCsv(file: File): Observable<any> {
    const formData = new FormData();
    formData.append('file', file, file.name);

    return this.http.post(`${this.apiUrl}/upload`, formData)
      .pipe(
        catchError(this.handleError)
      );
  }

  // Error handling function
  private handleError(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      // Client-side or network error
      console.error('An error occurred:', error.error.message);
    } 
    else {
      // Backend error
      console.error(
        `Backend returned code ${error.status},
        body was: ${JSON.stringify(error)}`);
    }
    return throwError(() => new Error('Something bad happened; please try again later.'));
  }
}
