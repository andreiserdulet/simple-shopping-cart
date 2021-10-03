import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { OrderComponent } from '../components/order/order.component';
import { ViewCartComponent } from '../components/view-cart/view-cart.component';

const routes: Routes = [
  { path: '', component: ViewCartComponent },
  { path: 'order', component: OrderComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CartRoutingModule { }
