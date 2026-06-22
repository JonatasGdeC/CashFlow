window.cashFlowCharts = {
    charts: {},

    renderMonthlyComparison(elementId, labels, expenses, incomes) {
        const element = document.getElementById(elementId);

        if (!element || typeof ApexCharts === "undefined") {
            return;
        }

        if (this.charts[elementId]) {
            this.charts[elementId].destroy();
        }

        const options = {
            chart: {
                type: "bar",
                height: 315,
                toolbar: { show: false },
                fontFamily: "Inter, ui-sans-serif, system-ui, -apple-system, BlinkMacSystemFont, Segoe UI, sans-serif"
            },
            colors: ["#df2e27", "#15803d"],
            dataLabels: { enabled: false },
            grid: {
                borderColor: "#e6e4dd",
                strokeDashArray: 4
            },
            legend: {
                position: "top",
                horizontalAlign: "left",
                fontWeight: 800,
                markers: { radius: 4 }
            },
            plotOptions: {
                bar: {
                    borderRadius: 5,
                    columnWidth: "58%"
                }
            },
            series: [
                { name: "Despesas", data: expenses },
                { name: "Rendimentos", data: incomes }
            ],
            tooltip: {
                y: {
                    formatter(value) {
                        return new Intl.NumberFormat("pt-BR", {
                            style: "currency",
                            currency: "BRL"
                        }).format(value);
                    }
                }
            },
            xaxis: {
                categories: labels,
                axisBorder: { color: "#e6e4dd" },
                axisTicks: { color: "#e6e4dd" },
                labels: {
                    style: {
                        colors: "#6f756f",
                        fontWeight: 800
                    }
                }
            },
            yaxis: {
                labels: {
                    formatter(value) {
                        return new Intl.NumberFormat("pt-BR", {
                            notation: "compact",
                            maximumFractionDigits: 1
                        }).format(value);
                    },
                    style: {
                        colors: "#6f756f",
                        fontWeight: 800
                    }
                }
            }
        };

        this.charts[elementId] = new ApexCharts(element, options);
        this.charts[elementId].render();
    }
};
