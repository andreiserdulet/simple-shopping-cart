import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CartService } from 'src/app/cart/services/cart.service';
import { Product } from 'src/app/core/model/product';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss']
})
export class ProductComponent implements OnInit {

  @Input()
  product!: Product;

  @Input()
  renderType: 'list' | 'details' | 'cart' = 'list';

  @Output() public selected: EventEmitter<number> = new EventEmitter();

  constructor(private cartService: CartService) { }

  ngOnInit(): void {
  }

  public selectProd(id: number){
    this.selected.emit(id);
  }

  public addCart(prodId: number) {
    this.cartService.addCart( prodId).subscribe();
  }

  public deleteFromCart(prodId: number) {
    this.cartService.removeFromCart(prodId).subscribe();
  }

}
