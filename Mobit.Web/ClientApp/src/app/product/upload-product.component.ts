import { Component, EventEmitter, Output } from '@angular/core';
import { ProductsService } from './products.service';
// TODO: Refatore
@Component({
  selector: 'app-upload',
  template: `
    <mat-card>
  <mat-card-header>
    <mat-card-title>Carregar dados por arquivo de texto</mat-card-title>
  </mat-card-header>
  <mat-card-content>
    <label class="input-file-label" for="fileInput">Selecionar Arquivo</label>
    <input id="fileInput" class="input-file" type="file" (change)="onFileSelected($event)" />    
  </mat-card-content>
  <mat-card-actions>
    <button mat-raised-button color="primary" (click)="onUpload()" [disabled]="!selectedFile">Upload</button>
  </mat-card-actions>
</mat-card>
  `,
  styles:[`
    .upload-container {
      display: flex;
      align-items: center;
    }
    .input-file-label {
      background-color: #3f51b5; /* Primary color */
      color: white;
      padding: 8px 16px;
      border-radius: 4px;
      cursor: pointer;
      font-family: 'Roboto', sans-serif;
      font-size: 14px;
      text-transform: uppercase;
      font-weight: 500;
      transition: background-color 0.3s ease;
    }

    .input-file-label:hover {
      background-color: #303f9f; /* Darker shade of primary color */
    }
    .input-file {
        display: none;
        
    }
  `]
})
export class UploadComponent {
  selectedFile: File | null = null;
  
  @Output() uploadSuccess = new EventEmitter<void>();
  
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
            this.uploadSuccess.emit();
        },
        error: (error) => {
            console.error('Error uploading file', error);            
        }
    });
    
  }
}
