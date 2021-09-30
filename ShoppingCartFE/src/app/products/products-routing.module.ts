import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductGuard } from '../core/service/product.guard';
import { ProductResolver } from '../core/service/product.resolver';
import { ProductDetailComponent } from './product-detail/product-detail.component';
import { ProductListComponent } from './product-list/product-list.component';

const routes: Routes = [
  { path: '', component: ProductListComponent },
  { path: 'details', 
    component: ProductDetailComponent,
    resolve: { product: ProductResolver },
    canActivate: [ProductGuard]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProductsRoutingModule { }
