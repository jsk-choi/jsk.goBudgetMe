import React from 'react';
import dtf from 'dateformat';
import DateBox from './DateBox.jsx';

const Transaction = React.createClass({
    componentDidMount () {
        //console.log('state', this.state);
        //this.setState(this.props.Transaction, () => {
        //    console.log('state', this.state);
        //});

        const tblHead = ['Date', 'Transaction', 'Amount', 'Posted', 'Balance', ''];
        this.setState({
            tblHead
        });
    },
    componentWillReceiveProps () {
        //this.replaceState(this.props.Transaction, () => {
        //    console.log('new props biotch', this.state);
        //});
    },
    handleBlur (e) {
        //const tg = e.target;
        //let tran = this.state;
        //tran[tg.id] = tg.value;
        //this.setState(tran, () => {
        //    console.log('state', this.state);
        //});
    },
    handleDateChange (dt) {
        //this.setState({ TransactionDate: dt.date }, () => {
        //    console.log('state', this.state);
        //});
    },
    handleEdit () {
        //let tran = this.state;
        //tran.inEdit = true;
        //this.setState(tran, () => {
        //    console.log('state', this.state);
        //});
    },
    handleSave () {
        //let tran = this.state;
        //tran.inEdit = false;
        //this.setState(tran, () => {
        //    console.log('state', this.state);
        //});
    },
    resetInEdit () {
        let idx = -1;
        if (this.state && this.state.inEdit)
        {
        }
    },
    render () {
        console.log(this);
        if (this.state) {
            return (
                <table>
                    <thead>
                        <tr>
                            {this.state.tblHead.map((v, i) => {
                                <th key={i}>{v}</th>
                            })}
                        </tr>
                    </thead>
                    <tbody>
                        {this.props.Transaction.map((v, i) => {
                            <tr key={i}>
                                <td>v.TransactionDate</td>
                                <td>v.TransactionDesc</td>
                                <td>v.Amount</td>
                                <td>v.Posted</td>
                                <td>v.Balance</td>
                            </tr>
                        })}
                    </tbody>
                </table>
            )
        }
        return null;
    }
});

export default Transaction;