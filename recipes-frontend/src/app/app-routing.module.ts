import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HeaderComponent } from './components/header/header.component';
import { RecipeInfoComponent } from './components/recipe-info/recipe-info.component';

const routes: Routes = [
  { path: 'home', component: HeaderComponent},
  { path: 'recipe/:id', component: RecipeInfoComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
