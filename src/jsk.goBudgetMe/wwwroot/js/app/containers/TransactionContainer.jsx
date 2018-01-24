/// <reference path="../../../_references.js" />

import React from 'react';
import _ from 'lodash';
import dtf from 'dateformat';
import DateBox from '../components/DateBox.jsx';
import Transaction from '../components/Transaction.jsx';
import TransactionUtil from '../utils/TransactionUtil.jsx';

import ut from '../utils/Util.jsx';

const TransactionContainer = React.createClass({
    dt (d) {
        return dtf(d, 'yyyy-mm-dd', true);
    },
    componentDidMount () {
    },
    getInitialState () {
        return {
            tblHead: ['Date', 'Transaction', 'Amount', 'Posted', 'Balance', ''],
            Transactions: [],
            TransactionEdit: {
                Transaction: {},
                idx: -1
            }
        }
    },
    componentWillReceiveProps (props) {
        this.setState({
            Transactions: props.Transactions
        });
    },
    handlePosted (e) {

        const tid = e.target.id;

        // DO NOTHING IF IN EDIT MODE
        if (Object.keys(this.state.TransactionEdit.Transaction).length !== 0) return;

        const tran = _.find(_.cloneDeep(this.state.Transactions), (t) => {
            return t.TransactionId == tid;
        });
        tran.Posted = !tran.Posted;

        TransactionUtil.set(tran).then((resp) => {
            this.props.handleChange();
        });
    },
    handleBlur (e) {
        const clonedTransactionEdit = _.cloneDeep(this.state.TransactionEdit);
        clonedTransactionEdit.Transaction[e.target.id] = e.target.value;
        this.setState({
            TransactionEdit: clonedTransactionEdit
        });
    },
    handleDateSelect (dt) {
        const clonedTransactionEdit = _.cloneDeep(this.state.TransactionEdit);
        clonedTransactionEdit.Transaction.TransactionDate = dt.date

        this.setState({
            TransactionEdit: clonedTransactionEdit
        });
    },
    handleEdit (e) {

        // PERSIST SYNTHETIC EVENT https://fb.me/react-event-pooling 
        e.persist();

        // CLOSE EDIT STATE IF EXISTS

        // NOTHING BEING EDITED, EXIT
        if (Object.keys(this.state.TransactionEdit.Transaction).length === 0) {
            this.setEdit(e);
            return;
        }

        // DEEP CLONE STATE TO AVOID DIRECT STATE MUTATION
        const clonedState = _.cloneDeep(this.state);
        clonedState.Transactions[this.state.TransactionEdit.idx].inEdit = false;
        this.replaceState(clonedState, () => {
            this.setEdit(e);
        });
    },
    handleDelete (e) {
        if (confirm("Are you sure you want to delete this transaction?")) {
            TransactionUtil.del(e.target.name)
                .then((tran) => {
                    this.props.handleChange();
                });
        }
    },
    handleKeyPress (e) {
        if (e.key == 'Enter') {
            this.handleSave();
        }
    },
    handleSave (e) {
        TransactionUtil.set(this.state.TransactionEdit.Transaction)
            .then((tran) => {
                this.state.TransactionEdit = {
                    Transaction: {},
                    idx: -1
                };
                this.props.handleChange();
            });
    },
    setEdit (e) {
        const clonedState = _.cloneDeep(this.state);
        const tid = e.target.id.split('_')[1];
        const idx = _.findIndex(this.state.Transactions, (v) => { 
            return v.TransactionId == tid;
        });
        const tran = clonedState.Transactions[idx];
        tran.inEdit = true;

        const tranEdit = {
            Transaction: tran,
            idx
        }
        clonedState.TransactionEdit = tranEdit;
        clonedState.Transactions[idx] = tran;

        this.replaceState(clonedState);
    },
    render () {
        if (this.state && this.state.Transactions) {
            return (
                <table>
                    <thead>
                        <tr>
                            {this.state.tblHead.map((v, i) => {
                                return <th key={i}>{v}</th>
                            })}
                        </tr>
                    </thead>
                    <tbody>
                        {this.state.Transactions.map((v, i) => {
                            return (
                                <tr key={i}>
                                    <td>
                                        {
                                            v.inEdit ? 
                                                <DateBox DateBoxName={'dt_' + v.UniqueId} 
                                                         SetDate={this.dt(v.TransactionDate)} 
                                                         onDateSelect={this.handleDateSelect} /> :
                                                this.dt(v.TransactionDate)
                                            
                                        }
                                    </td>
                                    <td>
                                        {
                                            v.inEdit ?
                                                <input id="TransactionDesc" 
                                                       type="text" 
                                                       defaultValue={v.TransactionDesc} 
                                                       onChange={this.handleBlur} /> :
                                                v.TransactionDesc
                                        }
                                    </td>
                                    <td>
                                        {
                                            v.inEdit ?
                                                <input id="Amount" 
                                                       type="number" 
                                                       className="text-right" 
                                                       defaultValue={v.Amount} 
                                                       onChange={this.handleBlur}
                                                       onKeyPress={this.handleKeyPress} /> :
                                                v.Amount
                                        }
                                    </td>
                                    <td>
                                        {
                                            v.Posted ?
                                                <div id={v.TransactionId} 
                                                     className="posted green-check" 
                                                     onClick={this.handlePosted} ></div> : 
                                                <div id={v.TransactionId} 
                                                     className="posted red-x" 
                                                     onClick={this.handlePosted} ></div>
                                        }
                                    </td>
                                    <td>{v.Balance}</td>
                                    <td>
                                        {
                                            v.inEdit ? 
                                                <div>
                                                    <img id={'t_' + v.TransactionId} 
                                                         src="/images/save.png"
                                                         className="edit-save" 
                                                         onClick={this.handleSave} />
                                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                                    <img id={'d_' + v.TransactionId} 
                                                         name={v.UniqueId}
                                                         src="/images/delete.png"
                                                         className="edit-save" 
                                                         onClick={this.handleDelete}></img>
                                                </div>
                                                      :
                                                <div>
                                                    <img id={'e_' + v.TransactionId} 
                                                         src="/images/edit.png"
                                                         className="edit-save" 
                                                         onClick={this.handleEdit}></img>
                                                </div>  
                                        }
                                    </td>
                                </tr>
                            )
                        })}
                    </tbody>
                </table>
            )
        }
        else {
            return null;
        }
    }
});

export default TransactionContainer;