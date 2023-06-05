import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HeaderComponent } from './components/header/header.component';
import { RecipeInfoComponent } from './components/recipe-info/recipe-info.component';
import { FilterComponent } from './components/filter/filter.component';
import { UserActionsComponent } from './components/user-actions/user-actions.component';
import { UserInfoComponent } from './components/user-info/user-info.component';

const routes: Routes = [
  { path: 'home', component: HeaderComponent},
  { path: 'recipe/:id', component: RecipeInfoComponent},
  { path: '', component: FilterComponent},
  { path: 'useractions', component: UserActionsComponent},
  { path: 'user/:id', component: UserInfoComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
