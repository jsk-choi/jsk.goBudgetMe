/// <reference path="~/lib/showdown/dist/showdown.js" />
/// <reference path="~/lib/react/react.js" />
var TransactionRow = React.createClass({
    render: function () {
        return React.createElement(
            "tr",
            { className: "transaction", uniqueId: this.props.data.UniqueId },
            React.createElement(
                "td",
                null,
                this.props.data.TransactionDate
            ),
            React.createElement(
                "td",
                null,
                this.props.data.TransactionDesc
            ),
            React.createElement(
                "td",
                null,
                this.props.data.Amount
            ),
            React.createElement(
                "td",
                null,
                this.props.data.Posted.toString()
            )
        );
    }
});

var TransactionTable = React.createClass({
    render: function () {
        var transactionNodes = this.props.data.map(function (transaction) {
            return React.createElement(TransactionRow, { data: transaction });
        });
        return React.createElement(
            "table",
            { className: "commentList" },
            transactionNodes
        );
    }
});

var CommentForm = React.createClass({
    handleSubmit: function (e) {
        e.preventDefault();

        var transactionDesc = this.refs.transactionDesc.value.trim();
        var transactionAmount = this.refs.transactionAmount.value.trim();

        this.props.onCommentSubmit({
            TransactionDate: Date.now(),
            TransactionDesc: transactionDesc,
            Amount: parseFloat(transactionAmount)
        });

        if (!transactionDesc || !transactionAmount) return;

        // TODO: SEND REQUEST TO SERVER
        this.refs.transactionDesc.value = '';
        this.refs.transactionAmount.value = '';
    },
    render: function () {
        return React.createElement(
            "form",
            { className: "commentForm", onSubmit: this.handleSubmit },
            React.createElement("input", { type: "text", ref: "transactionDesc", placeholder: "Description..." }),
            React.createElement("input", { type: "text", ref: "transactionAmount", placeholder: "Amount" }),
            React.createElement("input", { type: "submit", value: "post" })
        );
    }
});

var CommentBox = React.createClass({
    loadCommentsFromServer: function () {
        var xhr = new XMLHttpRequest();
        xhr.open('get', this.props.url, true);
        xhr.onload = function () {
            var data = JSON.parse(xhr.responseText);
            this.setState({ data: data });
            console.log('gotten: ' + JSON.stringify(this.state));
        }.bind(this);
        xhr.send();
    },
    handleTransactionSubmit: function (tran) {
        var dt = new Date();
        tran.TransactionDate = dt.getYear() + 1900 + '-' + (dt.getMonth() + 1) + '-' + dt.getDate() + ' ' + dt.getHours() + ':' + dt.getMinutes();
        console.log(tran);
        var xhr = new XMLHttpRequest();
        xhr.open('post', this.props.submitUrl, true);
        xhr.setRequestHeader("Content-Type", "application/json;");
        xhr.onload = function () {
            this.loadCommentsFromServer();
        }.bind(this);
        xhr.send(JSON.stringify(tran));
    },
    getInitialState: function () {
        return { data: [] };
    },
    componentDidMount: function () {
        this.loadCommentsFromServer();
        //timer = window.setInterval(this.loadCommentsFromServer, this.props.pollInterval);
    },
    render: function () {
        return React.createElement(
            "div",
            { className: "commentBox" },
            React.createElement(
                "h1",
                null,
                "Comments"
            ),
            React.createElement(TransactionTable, { className: "transaction", data: this.state.data }),
            React.createElement(CommentForm, { onCommentSubmit: this.handleTransactionSubmit })
        );
    }
});

ReactDOM.render(React.createElement(CommentBox, { url: "/api/values", submitUrl: "/api/values", pollInterval: 5000 }), document.getElementById('content'));