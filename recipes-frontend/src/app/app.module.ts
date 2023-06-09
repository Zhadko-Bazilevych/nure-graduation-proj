import { APP_INITIALIZER, ElementRef, NgModule, Renderer2 } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './components/header/header.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { OAuthModule } from 'angular-oauth2-oidc';
import { TokenInterceptorService } from './interceptors/token-interceptor.service';
import { RecipeInfoComponent } from './components/recipe-info/recipe-info.component';
import { GalleryComponent } from './components/recipe-info/gallery/gallery.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { FilterComponent } from './components/filter/filter.component';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgxSliderModule } from '@angular-slider/ngx-slider';
import { DifficultyComponent } from './components/common/bar/bar.component';
import { UserActionsComponent } from './components/user-actions/user-actions.component';
import { RecipeRowComponent } from './components/common/recipe-row/recipe-row.component';
import { AuthorRowComponent } from './components/common/author-row/author-row.component';
import { UserInfoComponent } from './components/user-info/user-info.component';
import { GlobalDataService } from './services/globalData.service';
import { RecipeUpdateComponent } from './components/recipe-update/recipe-update.component';
import { GalleryEditComponent } from './components/recipe-update/gallery-edit/gallery-edit.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    RecipeInfoComponent,
    GalleryComponent,
    GalleryEditComponent,
    FilterComponent,
    DifficultyComponent,
    UserActionsComponent,
    RecipeRowComponent,
    AuthorRowComponent,
    UserInfoComponent,
    RecipeUpdateComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    OAuthModule.forRoot(),
    FontAwesomeModule,
    NgMultiSelectDropDownModule.forRoot(),
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    NgxSliderModule,
  ],
  providers: [
    GlobalDataService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptorService,
      multi: true,
    },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
