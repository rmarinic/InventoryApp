import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-new-purchase',
  templateUrl: './new-purchase.component.html',
  styleUrl: './new-purchase.component.css'
})
export class NewPurchaseComponent implements OnInit {

  purchaseObj: any = {
    "purchaseId": 0,
    "purchaseDate": "2024-05-26T19:19:04.219Z",
    "productId": 0,
    "quantity": 0,
    "supplierName": "",
    "invoiceAmount": 0,
    "invoiceNo": ""
  };

  productList: any[] = [];

  constructor(private http:HttpClient) {};

  ngOnInit(): void {
    this.getAllProduct();
  }

  getAllProduct() {
    this.http.get('https://localhost:7047/api/Inventory/GetAllProduct').subscribe((res: any) => {
      this.productList = res.data;
    })
  }

  onSave(){
    this.http.post('https://localhost:7047/api/Inventory/CreateNewPurchase', this.purchaseObj).subscribe((res: any) => {
      if(res.result) {
        alert("Purchase completed successfully!")
      }else{
        alert(res.message)
      }
    },
    error=> {
      alert("API Error")
    })
  }

}
