import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-stock',
  templateUrl: './stock.component.html',
  styleUrl: './stock.component.css'
})
export class StockComponent implements OnInit{

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.getStock();
  }

  stockList: any [] = [];

  getStock(){
    this.http.get('https://localhost:7047/api/Inventory/GetAllStock').subscribe((res: any) => {
      this.stockList = res.data;
    })
  }
}
