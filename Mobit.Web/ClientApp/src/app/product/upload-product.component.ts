import { Component } from '@angular/core';
import { ProductsService } from './products.service';
// TODO: Refatore
@Component({
  selector: 'app-upload',
  template: `
    <h2>Upload CSV File</h2>
    <input type="file" (change)="onFileSelected($event)" />
    <button (click)="onUpload()" [disabled]="!selectedFile">Upload</button>
  `
})
export class UploadComponent {
  selectedFile: File | null = null;

  constructor(private productService: ProductsService) {}

  // File selection event
  onFileSelected(event: any) {
    this.selectedFile = event.target.files[0];
  }

  // Upload button click event
  onUpload() {
    if (!this.selectedFile) {
        return;
    }
    this.productService.uploadCsv(this.selectedFile).subscribe({
        next: (response) => {
            console.log('File uploaded successfully', response);            
        },
        error: (error) => {
            console.error('Error uploading file', error);            
        }
    });
    
  }
}
