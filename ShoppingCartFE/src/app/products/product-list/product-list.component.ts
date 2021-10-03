import { animate, style, transition, trigger } from '@angular/animations';
import { Component, OnInit } from '@angular/core';
import { Product } from '../../core/model/product';
import { ProductService } from '../../core/service/product.service';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.scss'],
  animations: [
    trigger('slideInOut', [
      transition(':enter', [
        style({transform: 'translateY(-100%)'}),
        animate('300ms ease-in', style({transform: 'translateY(0%)'}))
      ]),
      transition(':leave', [
        animate('300ms ease-in', style({transform: 'translateY(-100%)'}))
      ])
    ])
  ]
})
export class ProductListComponent implements OnInit {

  products: Product[] | undefined;
  public selectedProducts: number[] = [];
  public isLoading = true;

  constructor(private productService: ProductService) { }

  ngOnInit(): void {
    this.productService.loadProducts().subscribe(result => {
      this.products = result;
      this.isLoading = false;
    });
  }

  selected(event: number): void {
    this.selectedProducts.push(event);
  }

}
