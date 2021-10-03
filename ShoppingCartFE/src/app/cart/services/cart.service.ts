import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Cart } from '../model/cart';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  private readonly CART_ID = 'cart_id';
  private cart$ = new BehaviorSubject<Cart>({id: 0, products: []});

  constructor(private http: HttpClient) { 
    if (sessionStorage.getItem(this.CART_ID)) {
      this.loadCart();
    }
  }

  private readonly url = 'api/Cart';

  public getCart(): Observable<Cart> {
    return this.cart$.asObservable();
  }

  public clearCart() {
    return this.cart$.next({id: 0, products: []});
  }

  loadCart(cartId?: number) {
    let id = cartId;
    if (cartId || sessionStorage.getItem(this.CART_ID)) {
      if (!id) {
        id = Number(sessionStorage.getItem(this.CART_ID));
      }
    }
    return this.http.get<Cart>(`${this.url}/${id}`).subscribe(data => this.cart$.next(data));
  }

  addCart(productId: number, cartId?: number): Observable<Cart> {
    let id = cartId;
    if (cartId || sessionStorage.getItem(this.CART_ID)) {
      if (!id) {
        id = Number(sessionStorage.getItem(this.CART_ID));
      }
    }
    return this.http.post<Cart>(this.url, {
      productId: productId,
      cartId: id
    }).pipe(map(cart => {
      sessionStorage.setItem(this.CART_ID, '' + cart.id);
      this.cart$.next(cart);
      return cart;
    }));
  }

  removeFromCart(productId: number, cartId?: number): Observable<Cart> {
    let id = cartId;
    if (cartId || sessionStorage.getItem(this.CART_ID)) {
      if (!id) {
        id = Number(sessionStorage.getItem(this.CART_ID));
      }
    }
    return this.http.delete<Cart>(this.url, { body: {
      productId: productId,
      cartId: id
    }}).pipe(map(cart => {
      this.cart$.next(cart);
      return cart;
    }));
  }
}
