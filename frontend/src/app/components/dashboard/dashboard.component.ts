import { Component, OnInit, AfterViewChecked, ElementRef, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { ApiService } from '../../services/api.service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit, AfterViewChecked {
  stats: any = {
    totalProducts: 0,
    lowStockProducts: 0,
    totalSales: 0,
    totalPurchases: 0,
    recentSales: [],
    recentPurchases: [],
    salesTrend: []
  };
  isLoading = true;
  chartRendered = false;

  @ViewChild('trendCanvas') trendCanvas!: ElementRef<HTMLCanvasElement>;

  constructor(private apiService: ApiService) {}

  ngOnInit() {
    this.loadDashboardData();
  }

  ngAfterViewChecked() {
    if (this.stats.salesTrend?.length > 0 && this.trendCanvas && !this.chartRendered) {
      this.drawChart();
    }
  }

  loadDashboardData() {
    this.isLoading = true;
    this.apiService.getDashboardStats().subscribe({
      next: (data) => {
        this.stats = data;
        this.isLoading = false;
        this.chartRendered = false; // Reset to force redraw
      },
      error: (err) => {
        console.error('Error loading dashboard stats:', err);
        this.isLoading = false;
      }
    });
  }

  drawChart() {
    const canvas = this.trendCanvas.nativeElement;
    const ctx = canvas.getContext('2d');
    if (!ctx) return;

    this.chartRendered = true;

    // Resize canvas based on display width
    const rect = canvas.getBoundingClientRect();
    canvas.width = rect.width * window.devicePixelRatio;
    canvas.height = 200 * window.devicePixelRatio;
    ctx.scale(window.devicePixelRatio, window.devicePixelRatio);

    const width = rect.width;
    const height = 200;

    // Clear background
    ctx.clearRect(0, 0, width, height);

    const trend = this.stats.salesTrend || [];
    if (trend.length === 0) {
      ctx.fillStyle = '#646a86';
      ctx.font = '14px Inter';
      ctx.textAlign = 'center';
      ctx.fillText('No sales trend data available', width / 2, height / 2);
      return;
    }

    const maxVal = Math.max(...trend.map((t: any) => t.amount), 1000) * 1.1;
    const pointsCount = trend.length;
    const paddingLeft = 50;
    const paddingRight = 20;
    const paddingTop = 20;
    const paddingBottom = 30;

    const chartWidth = width - paddingLeft - paddingRight;
    const chartHeight = height - paddingTop - paddingBottom;

    // Draw grid lines
    ctx.strokeStyle = 'rgba(255, 255, 255, 0.05)';
    ctx.lineWidth = 1;
    for (let i = 0; i <= 4; i++) {
      const y = paddingTop + (chartHeight * i) / 4;
      ctx.beginPath();
      ctx.moveTo(paddingLeft, y);
      ctx.lineTo(width - paddingRight, y);
      ctx.stroke();

      // y-axis labels
      ctx.fillStyle = '#646a86';
      ctx.font = '10px Inter';
      ctx.textAlign = 'right';
      const labelVal = Math.round(maxVal - (maxVal * i) / 4);
      ctx.fillText(`₹${labelVal}`, paddingLeft - 10, y + 3);
    }

    // Plot line
    const coords: { x: number; y: number }[] = [];
    trend.forEach((item: any, i: number) => {
      const x = paddingLeft + (chartWidth * i) / Math.max(1, pointsCount - 1);
      const y = paddingTop + chartHeight - (chartHeight * item.amount) / maxVal;
      coords.push({ x, y });
    });

    // Draw Area gradient
    const gradient = ctx.createLinearGradient(0, paddingTop, 0, paddingTop + chartHeight);
    gradient.addColorStop(0, 'rgba(249, 115, 22, 0.3)');
    gradient.addColorStop(1, 'rgba(249, 115, 22, 0.0)');

    ctx.fillStyle = gradient;
    ctx.beginPath();
    ctx.moveTo(coords[0].x, paddingTop + chartHeight);
    coords.forEach(pt => ctx.lineTo(pt.x, pt.y));
    ctx.lineTo(coords[coords.length - 1].x, paddingTop + chartHeight);
    ctx.closePath();
    ctx.fill();

    // Draw Line
    ctx.strokeStyle = '#f97316';
    ctx.lineWidth = 3;
    ctx.beginPath();
    ctx.moveTo(coords[0].x, coords[0].y);
    for (let i = 1; i < coords.length; i++) {
      ctx.lineTo(coords[i].x, coords[i].y);
    }
    ctx.stroke();

    // Draw Dots and x-axis labels
    coords.forEach((pt, i) => {
      // Dots
      ctx.fillStyle = '#ffffff';
      ctx.beginPath();
      ctx.arc(pt.x, pt.y, 4, 0, Math.PI * 2);
      ctx.fill();
      ctx.strokeStyle = '#f97316';
      ctx.lineWidth = 2;
      ctx.stroke();

      // x labels
      const rawDate = new Date(trend[i].date);
      const label = rawDate.toLocaleDateString('en-IN', { day: '2-digit', month: 'short' });
      ctx.fillStyle = '#949ab4';
      ctx.font = '10px Inter';
      ctx.textAlign = 'center';
      ctx.fillText(label, pt.x, height - 10);
    });
  }
}
