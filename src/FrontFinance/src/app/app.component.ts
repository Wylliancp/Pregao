import { Component } from '@angular/core';
import {AppService} from './app.service';  
import { FormGroup, FormControl,Validators } from '@angular/forms';
import { Pregao } from './pregao';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Pregao';

  constructor(private AppService: AppService) { }  
  data: Pregao[];  
  PregaoForm: FormGroup;  
  submitted = false;   
  EventValue: any = "Save";   
  
  ngOnInit(): void {  
    this.getdata();  
    
  }  
  getdata() {  
    this.AppService.getData().subscribe((data: Pregao[]) => {  
      this.data = data;  
    })  
  }  
  postAddDados() {  
    this.AppService.postAddDados().subscribe((data: any) => {  
      console.log(data);  
    })  
  } 
  
  Save() {   
    this.submitted = true;  
    
     if (this.PregaoForm.invalid) {  
            return;  
     }  
    this.AppService.postData(this.PregaoForm.value).subscribe((data: any[]) => {  
      this.data = data;  
      this.resetFrom();  
  
    })  
  }  
  
  resetFrom()  
  {     
    this.getdata();  
    this.PregaoForm.reset();  
    this.EventValue = "Save";  
    this.submitted = false;   
  }
}

