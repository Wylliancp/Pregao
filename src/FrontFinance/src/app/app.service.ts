import { Injectable } from '@angular/core';
import { HttpClient,HttpHeaders }    from '@angular/common/http';  
import { Pregao } from './pregao'; 
import { Observable } from 'rxjs';  

@Injectable({
  providedIn: 'root'
})
export class AppService {
  
  readonly rootURL = 'https://localhost:5001/V1/Pregao';

  constructor(private http: HttpClient) { }  
      httpOptions = {  
        headers: new HttpHeaders({  
          'Content-Type': 'application/json'  
        })  
      }    
      getData(){  
        return this.http.get(this.rootURL + '/buscaPregao30Dias'); 
      }  
      postAddDados(){  
        return this.http.get(this.rootURL + '/Add');  
      } 
      
      postData(formData){  
        return this.http.post(this.rootURL + '/Add',formData);  
      }   
}