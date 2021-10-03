import { Component, OnInit } from '@angular/core';
import { Cart } from 'src/app/cart/model/cart';
import { CartService } from 'src/app/cart/services/cart.service';
import { trigger, animate, transition, style } from '@angular/animations';

@Component({
  selector: 'app-view-cart',
  templateUrl: './view-cart.component.html',
  styleUrls: ['./view-cart.component.scss'],
  animations: [
    trigger('fadeInAnimation', [
      transition(':enter', [
        style({ opacity: 0 }),
        animate('.3s', style({ opacity: 1 })),
      ]),
    ]),
  ],
})
export class ViewCartComponent implements OnInit {
  public cart!: Cart;
  public isLoading = true;

  constructor(private cartService: CartService) {}

  ngOnInit(): void {
    this.cartService.getCart().subscribe((data) => {
      this.cart = data;
      this.isLoading = false;
    });
  }
}
