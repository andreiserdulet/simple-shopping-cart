import { Component, OnInit } from '@angular/core';
import { Cart } from 'src/app/cart/model/cart';
import { CartService } from 'src/app/cart/services/cart.service';

@Component({
  selector: 'app-view-cart',
  templateUrl: './view-cart.component.html',
  styleUrls: ['./view-cart.component.scss']
})
export class ViewCartComponent implements OnInit {

  public cart!: Cart;
  public isLoading = true;

  constructor(private cartService: CartService) { }

  ngOnInit(): void {

    this.cartService.getCart().subscribe(data => {
      this.cart = data;
      this.isLoading = false;
    });
    

  }

}
