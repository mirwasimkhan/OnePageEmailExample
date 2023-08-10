/*!
* Start Bootstrap - Scrolling Nav v5.0.6 (https://startbootstrap.com/template/scrolling-nav)
* Copyright 2013-2023 Start Bootstrap
* Licensed under MIT (https://github.com/StartBootstrap/startbootstrap-scrolling-nav/blob/master/LICENSE)
*/
//
// Scripts
// 

const uri = 'https://localhost:7182/HomePageApi';

function sendEmail() {
    const emailTextbox = document.getElementById('exampleInputEmail1');
    const subjectTextbox = document.getElementById('exampleInputSubject');
    const messageTextbox = document.getElementById('exampleFormControlTextarea');
    const fullName = document.getElementById('exampleInputFullName');
    const element = document.getElementById('statusMessage');
    const elementFail = document.getElementById('emailFailed');
    const emailFrom = document.getElementById('exampleInputEmailFrom');

    const item = {
            "email": emailTextbox.value.trim(),
            "subject": subjectTextbox.value.trim(),
            "message": messageTextbox.value.trim(),
        "name": fullName.value.trim(),
        "messageFrom": emailFrom.value.trim()
        
    };
    console.log(item);
    if (emailTextbox.value != "") {
        fetch(uri, {
            method: 'POST',
            crossorigin: true,
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(item)
        })
            .then(response => response.json())
            .then(data => {
                if (data.response == true) {
                    element.removeAttribute("hidden");
                    elementFail.setAttribute("hidden");

                }
                else {
                    element.setAttribute("hidden", "hidden");
                    elementFail.removeAttribute("hidden");
                }            
            })
            .catch(error => console.error('Unable to send email.', error));
    }
}


window.addEventListener('DOMContentLoaded', event => {

    // Activate Bootstrap scrollspy on the main nav element
    const mainNav = document.body.querySelector('#mainNav');
    if (mainNav) {
        new bootstrap.ScrollSpy(document.body, {
            target: '#mainNav',
            rootMargin: '0px 0px -40%',
        });
    };

    // Collapse responsive navbar when toggler is visible
    const navbarToggler = document.body.querySelector('.navbar-toggler');
    const responsiveNavItems = [].slice.call(
        document.querySelectorAll('#navbarResponsive .nav-link')
    );
    responsiveNavItems.map(function (responsiveNavItem) {
        responsiveNavItem.addEventListener('click', () => {
            if (window.getComputedStyle(navbarToggler).display !== 'none') {
                navbarToggler.click();
            }
        });
    });

});

