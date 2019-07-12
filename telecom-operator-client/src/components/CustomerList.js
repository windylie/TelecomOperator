import React from 'react';
import api from '../apis/telecomOperator';

class CustomerList extends React.Component {
    state = { customerList : [] }

    async componentDidMount() {
        const response = await api.get('/customers');
        this.setState({ customerList : response.data });
    }

    render() {
        return (
            <select className="ui dropdown" onChange={e => this.props.onCustomerSelected(e.target.value)}>
                <option key="title" value="0">Select customer</option>
                {this.renderCustomerList()}
            </select>
        );
    }

    renderCustomerList() {
        return this.state.customerList.map ((customer) => {
            return (
                <option key={customer.id} value={customer.id}>{customer.name}</option>
            );
        });
    }
}

export default CustomerList;
