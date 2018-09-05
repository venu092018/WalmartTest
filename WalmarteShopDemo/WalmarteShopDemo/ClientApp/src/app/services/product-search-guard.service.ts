import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, Router } from '@angular/router';

@Injectable()
export class ProductSearchGuardService implements CanActivate {

  constructor(private _router: Router) { }

  canActivate(route: ActivatedRouteSnapshot): boolean {
    let searchQuery = route.queryParams["query"];
    if (searchQuery && searchQuery.length > 0 && searchQuery.trim().length > 0) {
      return true;
    }

    this._router.navigate(['/']);
    return false;

  }
}
