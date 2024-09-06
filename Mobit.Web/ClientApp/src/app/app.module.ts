import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './core/components/nav-menu/nav-menu.component';
import { HomeComponent } from './features/home/home.component';
import { ApiAuthorizationModule } from 'src/api-authorization/api-authorization.module';
import { AuthorizeGuard } from 'src/api-authorization/authorize.guard';
import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatCardModule } from '@angular/material/card';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { ReactiveFormsModule } from '@angular/forms';
import { ProductListComponent } from './features/product/components/product-list/product-list.component';
import { EditProductModalComponent } from './share/components/edit-product-modal/edit-product-modal.component';
import { AddProductModalComponent } from './share/components/add-product-modal/add-product-modal.component';
import { ProductDetailsComponent } from './features/product/components/product-details/product-details.component';
import { ProductsService } from './core/services/products.service';
import { UploadComponent  } from "./share/components/upload-product/upload-product.component";
@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    ProductListComponent,
    AddProductModalComponent,
    EditProductModalComponent,
    ProductDetailsComponent,
    UploadComponent
  ],
  imports: [
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatButtonModule,
    MatIconModule,
    MatToolbarModule,
    MatCardModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    ReactiveFormsModule,    
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ApiAuthorizationModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' }      
      ,{ path: 'products', component: ProductListComponent, canActivate: [AuthorizeGuard] }
      ,{ path: 'products/:id', component: ProductDetailsComponent, canActivate: [AuthorizeGuard] }
    ]),
    BrowserAnimationsModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true }
    ,ProductsService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
