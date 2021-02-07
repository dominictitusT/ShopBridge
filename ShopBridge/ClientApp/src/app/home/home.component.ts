import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { BackendService } from '../service/data.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {

  ProductForm: FormGroup;
  File;
  href;
  productList;
  Removedone = false;
  done = false;
  loader = false;
  View = false;
  ProductItem;
  constructor(private dataService: BackendService) {

  }

  ngOnInit() {
    this.dataService.GetData().subscribe(data => {
      if (data != null) {
        this.productList = data;
      }
    });
    this.href = window.location.href;
    this.ProductForm = new FormGroup({
      name: new FormControl('', [Validators.required]),
      price: new FormControl('', [Validators.required, Validators.pattern('[0-9]+(\.[0-9][0-9]?)?')]),
      description: new FormControl('', [Validators.required]),
      imgName: new FormControl(''),
      img: new FormControl('')
    })
  }

  add() {

    if (this.ProductForm.valid) {
      this.loader = true;
      var formData = new FormData();
      formData.append("model", JSON.stringify(this.ProductForm.value));
      formData.append("file", this.File);
      this.dataService.AddProduct(formData).subscribe(data => {
        if (data != null) {
          this.loader = false;
          this.done = true;
          this.productList = data;
          this.ProductForm.reset();
          setTimeout(() => {
            this.done = false;
          }, 500);
        }
      })
    }
    else {
      this.ProductForm.markAllAsTouched();
    }
  }

  remove(id) {
    this.loader = true;
    this.dataService.RemoveProduct(id).subscribe(data => {
      if (data != null) {
        this.loader = false;
        this.Removedone = true;
        this.productList = data;
        setTimeout(() => {
          this.Removedone = false;
        }, 500);
      }
    });
  }

  resetForm() {
    this.ProductForm.reset();
  }

  onSelectfile(event) {

    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      this.File = file;
    }
  }

  ViewProduct(item) {
    this.View = true;
    this.ProductItem = item;
  }

  back(event)
  {
    this.View=false;
  }


}
