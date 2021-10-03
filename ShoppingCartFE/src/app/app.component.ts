import { Component } from '@angular/core';
import { CartService } from './cart/services/cart.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  public cartItemsNo = 0;

  title = 'ShoppingCartFE';

  constructor (private cartService: CartService) {
    this.cartService.getCart().subscribe(cart => this.cartItemsNo = cart.products.length);
  }
}
