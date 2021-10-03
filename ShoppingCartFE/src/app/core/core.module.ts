import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ElipsisPipe } from './pipe/elipsis.pipe';
import { ProductComponent } from './product/product.component';
import { BackgroundDirective } from './directive/background.directive';
import { ProductService } from './service/product.service';
import { RouterModule } from '@angular/router';
import { ErrorPageComponent } from './error-page/error-page.component';
import { FaIconComponent, FaIconLibrary, FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { far } from '@fortawesome/free-regular-svg-icons';
import { fas } from '@fortawesome/free-solid-svg-icons';

@NgModule({
  declarations: [
    ElipsisPipe,
    ProductComponent,
    BackgroundDirective,
    ErrorPageComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    FontAwesomeModule

  ],
  exports : [
    ElipsisPipe,
    ProductComponent,
    BackgroundDirective,
    ErrorPageComponent,
    FaIconComponent
  ]
})
export class CoreModule {
  constructor(library: FaIconLibrary) {
    library.addIconPacks(far, fas);
  }
 }
