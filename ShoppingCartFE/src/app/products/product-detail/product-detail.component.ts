import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Product } from 'src/app/core/model/product';
import { ProductService } from 'src/app/core/service/product.service';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.scss']
})
export class ProductDetailComponent implements OnInit {

  public product!: Product;
  public isLoading = true;

  constructor(private productService: ProductService,
    private route: ActivatedRoute) { }

  ngOnInit(): void {
    /*
    const productId = Number(this.route.snapshot.queryParams.productId);
    this.productService.loadProducts().subscribe( products => {
      const prod = products.find(prod => prod.id === productId);
      if (prod) {
        this.product = prod;
        this.isLoading = false;
      }
    });
    */
    this.route.data.subscribe(data => {
      this.product = data.product;
      this.isLoading = false;
    });
  }

}
