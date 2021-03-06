import { Injectable, Inject } from "@angular/core"
import { HttpClient, HttpErrorResponse } from "@angular/common/http"
import { Observable } from "rxjs/Observable"
import 'rxjs/add/operator/catch'
import 'rxjs/add/operator/do'
import { IProductInfoFull } from "../models/product-info-full";

@Injectable()
export class ProductLookupService {
  constructor(private _http: HttpClient, @Inject('BASE_URL') private _baseUrl: string) {

  }

  getProductByItemId(itemId: number): Observable<IProductInfoFull> {
    return this._http.get<IProductInfoFull>(this._baseUrl + 'api/WalmartProductData/ProductLookup?id=' + itemId)
      .do(data => {
      })
      .catch(this.handleError);
  }

  private handleError(err: HttpErrorResponse) {
    console.log(err.message);
    return Observable.throw(err.message);
  }
}
