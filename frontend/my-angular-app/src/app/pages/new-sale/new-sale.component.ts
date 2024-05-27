import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-new-sale',
  templateUrl: './new-sale.component.html',
  styleUrl: './new-sale.component.css'
})
export class NewSaleComponent implements OnInit{
  saleObj: any = {
    "saleId": 0,
    "invoiceNo": "",
    "customerName": "",
    "phoneNumber": "",
    "saleDate": "2024-05-26T19:46:01.917Z",
    "productId": 0,
    "quantity": 0,
    "totalAmount": 0
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
    this.http.post('https://localhost:7047/api/Inventory/CreateNewSale', this.saleObj).subscribe((res: any) => {
      if(res.result) {
        alert("Sale completed successfully!")
      }else{
        alert(res.message)
      }
    },
    error=> {
      alert("API Error")
    })
  }

  checkStock(){
    this.http.get('https://localhost:7047/api/Inventory/CheckStockByProductId?productId=' + this.saleObj.productId).subscribe((res: any) => {
      if(!res.result){
        alert("Product not available in stock");
      }else{
        this.saleObj = res.data;
      }
    })
  }
}
