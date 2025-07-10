"use strict";

//let btnss = document.querySelectorAll(".delete-brand");
//btnss.forEach((btn) => {
//    btn.addEventListener("click", function () {
//        let brandId = parseInt(this.getAttribute("data-id"));
//        deleteBrand(brandId)
//        this.closest("tr").remove();
//    })

//});


//async function deleteBrand(brandId) {
//    try {
//        const response = await fetch(`/admin/brand/delete?id=${brandId}`, {
//            method: `POST`,
//            headers: {
//                'Content-Type': `application/json`
//            },
//        });
//        if (response.ok) {
//            console.log("Brand deleted");
//        } else {
//            console.eror("failed to deleted . Status", response.status);
//        }

//    } catch (eror) {
//        console.error("Fetch error:", error);
//    }
//}

//document.getElementById('subscribeBtn').addEventListener('click', function () {
//    const emailInput = document.getElementById('subscribeEmail');
//    const email = emailInput.value.trim();
//    const button = this;

//    // Email validation
//    if (!email) {
//        alert('Zəhmət olmasa email ünvanı daxil edin!');
//        return;
//    }

//    if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email)) {
//        alert('Zəhmət olmasa düzgün email ünvanı daxil edin!');
//        return;
//    }

//    // Disable button during request to prevent multiple submissions
//    button.disabled = true;
//    button.innerHTML = 'Göndərilir...';

//    // Send to API
//    fetch('/api/admin/NewsLetter/AddEmail', {
//        method: 'POST',
//        headers: {
//            'Content-Type': 'application/json',
//        },
//        body: JSON.stringify({ email: email })
//    })
//        .then(response => {
//            if (response.ok) {
//                return response.json();
//            }
//            throw new Error('Network response was not ok');
//        })
//        .then(data => {
//            alert('Uğurla abunə oldunuz!');
//            emailInput.value = ''; // Clear input
//        })
//        .catch(error => {
//            console.error('Error:', error);
//            alert('Xəta baş verdi, zəhmət olmasa yenidən cəhd edin.');
//        })
//        .finally(() => {
//            // Re-enable button
//            button.disabled = false;
//            button.innerHTML = 'Subscribe <i class="far fa-search"></i>';
//        });
//});



document.addEventListener("DOMContentLoaded", function () {
    const passwordInput = document.getElementById("userPassword");
    const togglePassword = document.getElementById("togglePassword");

    togglePassword.addEventListener("click", function () {
        if (passwordInput.type === "password") {
            passwordInput.type = "text";
            togglePassword.classList.remove("fa-eye");
            togglePassword.classList.add("fa-eye-slash");
        } else {
            passwordInput.type = "password";
            togglePassword.classList.remove("fa-eye-slash");
            togglePassword.classList.add("fa-eye");
        }
    });
});


document.addEventListener("DOMContentLoaded", function () {
    const passwordInput = document.getElementById("userPassword");
    const togglePassword = document.querySelector(".toggle-password-icon");

    togglePassword.addEventListener("click", function () {
        if (passwordInput.type === "password") {
            passwordInput.type = "text";
            togglePassword.classList.remove("fa-eye");
            togglePassword.classList.add("fa-eye-slash");
        } else {
            passwordInput.type = "password";
            togglePassword.classList.remove("fa-eye-slash");
            togglePassword.classList.add("fa-eye");
        }
    });
});

