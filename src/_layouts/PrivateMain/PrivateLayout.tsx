import React, { Component } from 'react';

import TopBar from './TopBar';

class PrivateLayout extends Component {
    render() { 
        return(
            <div>
                <TopBar />
                <div>Sidebar Navigation</div>
                <main>
                    {this.props.children}
                    <div>
                        Footer
                        <br/><br/><br/><hr/><br/><br/>
                        <div>
                            <p>
                                <a href="https://chakian.com" target="_blank">Chakian</a>
                            </p>
                            <p>
                                <a href="https://cagdaskorkut.com" target="_blank">Çağdaş Korkut</a>
                            </p>
                        </div>
                    </div>
                </main>
            </div>
        );
    }
}

export { PrivateLayout };