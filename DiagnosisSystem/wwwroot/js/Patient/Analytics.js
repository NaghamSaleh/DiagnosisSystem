    var specName = @Html.Raw(Json.Serialize(specName));
    var count = @Html.Raw(Json.Serialize(count));

    var ctx = document.getElementById('barChart').getContext('2d');
    var myChart = new Chart(ctx, {
        type: 'bar',
    data: {
        labels: specName,
    datasets: [{
        label: 'Doctors per Specialty',
    data: count,
    backgroundColor: 'rgba(153, 102, 255, 0.5)',
    borderColor: 'rgba(153, 102, 255, 1)',
    borderWidth: 1
                }]
            },
    options: {
        scales: {
        y: {
        beginAtZero: true,
    ticks: {
        stepSize: 1
                        }
                    }
                }
            }
        });
