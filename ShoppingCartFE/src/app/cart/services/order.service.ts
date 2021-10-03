import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Order } from '../model/order';
import { CartService } from './cart.service';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  private readonly CART_ID = 'cart_id';

  constructor(private http: HttpClient,
    private cartService: CartService) { 

  }

  private readonly url = 'api/Order';

  sendOrder(order: Order) {
    return this.http.post<Order>(this.url, order).subscribe(() => {
      this.cartService.clearCart();
      sessionStorage.removeItem(this.CART_ID)
    });
  }
}
