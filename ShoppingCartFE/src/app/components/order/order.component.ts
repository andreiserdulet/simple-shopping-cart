import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Cart } from 'src/app/cart/model/cart';
import { CartService } from 'src/app/cart/services/cart.service';
import { OrderService } from 'src/app/cart/services/order.service';
import { NgForm } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  template: `
  {{f.value |json}}
    <form #f="ngForm" (ngSubmit) = "onSubmit(data)" novalidate>
      <input type="text" name="name" ngModel placeholder="Name">
      <br></br>
      <input type="text" name="adress" ngModel placeholder="Adress">
      <br></br>
      <input type="text" name="email" ngModel placeholder="Email">
      <br></br>
      <input type="text" name=" phoneNo" ngModel placeholder="Phone Number">
      <input type="number" name="id" ngModel placeholder="Idn" readonly>
      <br></br>
      <p> </p>
    </form>

  `,
  styleUrls: ['./order.component.scss'],
})
export class OrderComponent implements OnInit {
  public readonly emailPattern = '^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$';
  public readonly url = 'https://schoppingcart.azurewebsites.net/api/Order';
  public form!: FormGroup;
  public cart!: Cart;

  constructor(
    private formBuilder: FormBuilder,
    private cartService: CartService,
    private orderService: OrderService,
    private http: HttpClient
  ) {}
  onSubmit(data: NgForm) {
    this.http.post(this.url, data);
    console.log(data.value);
  }
  ngOnInit(): void {
    this.createForm();
    this.cartService.getCart().subscribe((data) => {
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
      cartId: ['', Validators.required],
    });
  }

  public sendOrder() {
    this.orderService.sendOrder({
      name: this.form.controls['name'].value,
      address: this.form.controls['address'].value,
      email: this.form.controls['email'].value,
      phoneNo: '' + this.form.controls['phoneNo'].value,
      cartId: this.form.controls['cartId'].value,
    });
  }
}
