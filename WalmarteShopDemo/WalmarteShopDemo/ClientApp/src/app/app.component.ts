import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'eShop Demo';

  _productSearchQuery: string;
  get productSearchQuery(): string {
    return this._productSearchQuery;
  }
  set productSearchQuery(value: string) {
    this._productSearchQuery = value;
  }

  constructor(private _router: Router) { }

  onSearch(): void {
    let searchQuery: string = this._productSearchQuery;
    if (searchQuery && searchQuery.length > 0 && searchQuery.trim().length > 0) {
      console.log(searchQuery);
      this._router.navigate(['/products'], { queryParams: { query: this._productSearchQuery } });
    }
  }
}
