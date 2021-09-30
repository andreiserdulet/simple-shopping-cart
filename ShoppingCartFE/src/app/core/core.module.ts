import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ElipsisPipe } from './pipe/elipsis.pipe';
import { ProductComponent } from './product/product.component';
import { BackgroundDirective } from './directive/background.directive';
import { ProductService } from './service/product.service';
import { RouterModule } from '@angular/router';
import { ErrorPageComponent } from './error-page/error-page.component';



@NgModule({
  declarations: [
    ElipsisPipe,
    ProductComponent,
    BackgroundDirective,
    ErrorPageComponent
  ],
  imports: [
    CommonModule,
    RouterModule
  ],
  exports : [
    ElipsisPipe,
    ProductComponent,
    BackgroundDirective,
    ErrorPageComponent
  ]
})
export class CoreModule { }
