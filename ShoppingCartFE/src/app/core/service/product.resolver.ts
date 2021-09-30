import { Injectable } from '@angular/core';
import {
  Resolve,
  RouterStateSnapshot,
  ActivatedRouteSnapshot
} from '@angular/router';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { Product } from '../model/product';
import { ProductService } from './product.service';

@Injectable({
  providedIn: 'root'
})
export class ProductResolver implements Resolve<Observable<Product>> {

  constructor (private productService: ProductService) {}

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<Product> {
    const productId = Number(route.queryParams.productId);
    return this.productService.loadProducts().pipe( map(products => {
      const prod = products.find(prod => prod.id === productId);
      if (prod) {
        return prod;
      }
      return products[0];
    }));
  }
}
