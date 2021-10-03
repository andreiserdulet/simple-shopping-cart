import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CartRoutingModule } from './cart-routing.module';
import { ReactiveFormsModule } from '@angular/forms';
import { OrderComponent } from '../components/order/order.component';
import { ViewCartComponent } from '../components/view-cart/view-cart.component';
import { CoreModule } from '../core/core.module';



@NgModule({
  declarations: [
    ViewCartComponent,
    OrderComponent],
  imports: [
    CommonModule,
    CoreModule,
    CartRoutingModule,
    ReactiveFormsModule
  ]
})
export class CartModule { }
