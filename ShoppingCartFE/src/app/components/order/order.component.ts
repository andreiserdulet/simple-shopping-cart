import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Cart } from 'src/app/cart/model/cart';
import { CartService } from 'src/app/cart/services/cart.service';
import { OrderService } from 'src/app/cart/services/order.service';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.scss']
})
export class OrderComponent implements OnInit {

  public readonly emailPattern = '^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$';

  public form!: FormGroup;
  public cart!: Cart;

  constructor(private formBuilder: FormBuilder,
    private cartService: CartService,
    private orderService: OrderService) { }

  ngOnInit(): void {
    this.createForm();
    this.cartService.getCart().subscribe( data => {
      this.cart;
      this.form.controls['cartId'].setValue(data.id);
    });
  }

  private createForm() {
    this.form = this.formBuilder.group({
      name: ['', Validators.required],
      address: ['', Validators.required],
      email: ['', Validators.required],
      phoneNo: ['', Validators.required],
      cartId: ['', Validators.required]
    });
  }

  public sendOrder() {
    this.orderService.sendOrder({
      name: this.form.controls['name'].value,
      address: this.form.controls['address'].value,
      email: this.form.controls['email'].value,
      phoneNo: '' + this.form.controls['phoneNo'].value,
      cartId: this.form.controls['cartId'].value
    });
  }
}
