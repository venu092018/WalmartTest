import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProductLookupService } from '../services/product-lookup.service';
import { ProductRecommendationService } from '../services/product-recommendations.service';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/forkJoin';
import { IProductInfoFull } from '../models/product-info-full';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.css']
})
export class ProductDetailComponent implements OnInit {
  errorMessage: string = "";
  product: IProductInfoFull;
  recommendedProducts: IProductInfoFull[];

  waitingOnDataLoad: boolean = false;
  _currentproductItemId: number;

  get currentproductItemId(): number {
    return this._currentproductItemId;
  }
  set currentproductItemId(value: number) {
    this._currentproductItemId = value;
    this.loadProductInfo();
  }

  constructor(private _route: ActivatedRoute,
    private _productLookupService: ProductLookupService,
    private _productRecommendationService: ProductRecommendationService) {

  }

  ngOnInit() {
    this._route.params.subscribe(params => {
      this.currentproductItemId = +params["id"];
    });
  }

  loadProductInfo() {
    this.waitingOnDataLoad = true;
    var productsTask = this._productLookupService.getProductByItemId(this.currentproductItemId);
    var recommendedProductsTask = this._productRecommendationService.getRecommendations(this.currentproductItemId);
    Observable.forkJoin([productsTask, recommendedProductsTask])
      .subscribe(apiResponse => {
        this.product = apiResponse[0];
        this.recommendedProducts = apiResponse[1];
      },
        error => { this.errorMessage = <any>error },
        () => { this.waitingOnDataLoad = false; });
  }
}
