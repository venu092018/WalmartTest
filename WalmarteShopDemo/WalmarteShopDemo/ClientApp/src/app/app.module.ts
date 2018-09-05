import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { ProductDetailComponent } from './product-detail/product-detail.component';
import { ProductSearchComponent } from './product-search/product-search.component';
import { ProductSearchService } from './services/product-search.service';
import { ProductGuardService } from './services/product-guard.service';
import { ProductSearchGuardService } from './services/product-search-guard.service';
import { ProductLookupService } from './services/product-lookup.service';
import { ProductRecommendationService } from './services/product-recommendations.service';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    ProductDetailComponent,
    ProductSearchComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'products', canActivate: [ProductSearchGuardService], component: ProductSearchComponent },
      { path: 'products/:id', canActivate: [ProductGuardService], component: ProductDetailComponent }
    ])
  ],
  providers: [ProductSearchService,
    ProductGuardService,
    ProductSearchGuardService,
    ProductLookupService,
    ProductRecommendationService],
  bootstrap: [AppComponent]
})
export class AppModule { }
