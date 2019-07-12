import React from 'react';
import api from '../apis/telecomOperator';

class CustomerPhoneList extends React.Component {
    state = { inputCustomerId : '', phoneList : [] }

    onInputChange = (event) => {
        this.setState({ inputCustomerId: event.target.value });
    }

    onBtnClick = async () => {
        const response = await api.get('/customers/' + this.state.inputCustomerId + '/phones');
        this.setState({ phoneList : response.data });
    }

    getIcon(activated)
    {
        if (activated)
            return <i className="icon checkmark" />

        return <i className="exclamation icon" />
    }

    renderPhoneList()
    {
        const phoneList = this.state.phoneList;

        if (!phoneList || phoneList.length === 0) {
            return (
                <tr>
                    <td colSpan="2">
                        No records found
                    </td>
                </tr>);
        }

        //<i class="icon checkmark"></i> Approved
        return phoneList.map ((item) => {
            return (
                <tr>
                    <td>{item.phoneNo}</td>
                    <td>{this.getIcon(item.activated)}{item.activated ? "Activated" : "Inactive"}</td>
                </tr>
                // <div className="item" key={"phone-" + item.id }>
                //     <div className="right floated content">
                //         <div className="ui label">{item.activated ? "Activated" : "Inactive"}</div>
                //     </div>
                //     <div className="content">{item.phoneNo}</div>
                // </div>
            );
        });
    }

    render() {
        return (
            <div>
                <div className="ui action input">
                    <input type="text"
                        placeholder="Input Customer Id..."
                        onChange={this.onInputChange}
                        value={this.state.inputCustomerId} />
                    <button className="ui button"
                            onClick={this.onBtnClick}>
                            Show Phones
                    </button>
                </div>
                <table class="ui very basic table">
                    <tbody>
                        {this.renderPhoneList()}
                    </tbody>
                </table>
            </div>
        );
    };

};

export default CustomerPhoneList;
