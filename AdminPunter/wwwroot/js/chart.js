google.charts.load('current', { packages: ['corechart', 'line'] });
google.charts.setOnLoadCallback(initChart);

function initChart()
{
    var xs = [];
    var ys = [];
    window.generateChart({xs, ys});
}

window.generateChart = (params) =>
{
    var xs = params.xs;
    var ys = params.ys;

    var data = new google.visualization.DataTale();

    data.addColumn('number', 'X');
    data.addColumn('number', 'Y');

    for (var i = 0; i < ys.length; i++)
    {
        data.addRow([xs[i]])
    }

}