import React from 'react';

const PrivateLayout = ({ children }) => {
    return (
        <div>
            <div>Top Bar</div>
            <div>Sidebar Navigation</div>
            <main>
                {children}
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

export { PrivateLayout };