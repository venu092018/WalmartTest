import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProductSearchService } from '../services/product-search.service';
import { IProductInfoFull } from '../models/product-info-full';

@Component({
  selector: 'app-product-search',
  templateUrl: './product-search.component.html',
  styleUrls: ['./product-search.component.css']
})
export class ProductSearchComponent implements OnInit {
  _productSearchQuery: string;

  waitingOnDataLoad: boolean = false;

  errorMessage: string = "";
  public products: IProductInfoFull[];

  get productSearchQuery(): string {
    return this._productSearchQuery;
  }
  set productSearchQuery(value: string) {
    this._productSearchQuery = value;
    this.loadProducts();
  }

  constructor(private _route: ActivatedRoute, private _productSearchService: ProductSearchService) { }

  ngOnInit() {

    this._route.queryParams.subscribe(queryParams => {
      this.productSearchQuery = queryParams["query"];
    });

  }

  loadProducts() {
    this.waitingOnDataLoad = true;
    this._productSearchService.getProducts(this.productSearchQuery)
      .subscribe(products => {
        this.products = products;
      },
        error => { this.errorMessage = <any>error; },
        () => { this.waitingOnDataLoad = false; });
  }
}
