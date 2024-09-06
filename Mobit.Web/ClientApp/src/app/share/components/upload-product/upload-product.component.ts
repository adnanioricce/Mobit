import { Component, EventEmitter, Output } from '@angular/core';
import { ProductsService } from '../../../core/services/products.service';
// TODO: Refatore
@Component({
  selector: 'app-upload',
  templateUrl: './upload-product.component.html',
  styleUrls: ['./upload-product.component.css']
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
