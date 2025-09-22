<%-- Home.aspx --%>
<%@ Page Title="Home - SPC Pharmacy" Language="C#" MasterPageFile="~/Spc.master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Spc_web.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%-- Page-specific styles for a dynamic and engaging homepage --%>
    <style>
        /* --- Hero Section --- */
        .hero-section {
            padding: 100px 0;
            background: linear-gradient(rgba(0, 123, 255, 0.6), rgba(0, 98, 204, 0.8)), url('https://images.unsplash.com/photo-1584308666744-24d5c474f2ae?q=80&w=2670&auto=format&fit=crop') center center / cover no-repeat;
            color: #ffffff;
            border-radius: 0 0 var(--border-radius) var(--border-radius);
            margin-top: -1rem; /* Pulls the hero up slightly to bleed into the navbar area */
        }

        .hero-content h1 {
            font-size: clamp(2.5rem, 5vw, 4rem); /* Responsive font size */
            font-weight: 700;
            text-shadow: 2px 2px 8px rgba(0, 0, 0, 0.5);
            animation: fadeInDown 1s ease-out;
        }

        .hero-content p {
            font-size: clamp(1.1rem, 2.5vw, 1.25rem);
            font-weight: 300;
            max-width: 650px;
            margin: 20px auto 40px;
            animation: fadeInUp 1s ease-out 0.3s;
            animation-fill-mode: both;
        }

        .hero-buttons .btn {
            padding: 12px 30px;
            font-weight: 600;
            border-radius: 50px; /* Modern pill-shaped buttons */
            transition: all 0.3s ease;
            animation: fadeInUp 1s ease-out 0.6s;
            animation-fill-mode: both;
        }

        .hero-buttons .btn-primary {
             background-color: var(--secondary-color); /* Use the vibrant green */
             border-color: var(--secondary-color);
        }
        
        .hero-buttons .btn-primary:hover {
            transform: translateY(-3px);
            box-shadow: 0 10px 20px rgba(0, 0, 0, 0.2);
        }

        .hero-buttons .btn-outline-light:hover {
            background-color: #fff;
            color: var(--primary-color);
            transform: translateY(-3px);
        }

        /* --- Section Styling --- */
        .section-title {
            margin-bottom: 60px;
        }

        .section-title h2 {
            font-weight: 700;
            position: relative;
            padding-bottom: 15px;
            display: inline-block;
        }

        .section-title h2:after {
            content: '';
            position: absolute;
            bottom: 0;
            left: 50%;
            transform: translateX(-50%);
            width: 80px;
            height: 4px;
            background: var(--primary-color);
            border-radius: 2px;
        }

        /* --- Features Section --- */
        .features-section {
            padding: 80px 0;
        }
        
        .feature-card {
            border: none;
            border-radius: var(--border-radius);
            padding: 40px 30px;
            transition: all 0.4s ease;
            background-color: #ffffff;
            box-shadow: var(--box-shadow);
        }

        .feature-card:hover {
            transform: translateY(-10px);
            box-shadow: 0 20px 40px rgba(0, 0, 0, 0.12);
        }
        
        .feature-icon-wrapper {
            display: inline-flex;
            align-items: center;
            justify-content: center;
            width: 80px;
            height: 80px;
            border-radius: 50%;
            background: linear-gradient(135deg, var(--primary-color), var(--secondary-color));
            color: #fff;
            font-size: 2.5rem;
            margin-bottom: 25px;
            transition: all 0.3s ease;
        }
        
        .feature-card:hover .feature-icon-wrapper {
            transform: scale(1.1) rotate(-15deg);
            box-shadow: 0 10px 20px rgba(0, 123, 255, 0.3);
        }

        /* --- Why Choose Us Section --- */
        .why-us-section {
            padding: 80px 0;
            background-color: #fff;
        }
        .why-us-section img {
            border-radius: var(--border-radius);
            box-shadow: var(--box-shadow);
        }
        .why-us-list li {
            font-size: 1.1rem;
            margin-bottom: 1rem;
        }
        .why-us-list .fa-check-circle {
            color: var(--secondary-color);
            margin-right: 10px;
        }

        /* --- Call to Action Section --- */
        .cta-section {
            padding: 80px 0;
            border-radius: var(--border-radius);
            color: white;
            background: linear-gradient(135deg, var(--primary-color), var(--dark-color));
            position: relative;
            overflow: hidden;
        }
        /* Subtle background pattern */
        .cta-section::before {
            content: '';
            position: absolute;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background-image: url("data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='40' height='40' viewBox='0 0 40 40'%3E%3Cg fill-rule='evenodd'%3E%3Cg fill='%23ffffff' fill-opacity='0.1'%3E%3Cpath d='M0 38.59l2.83-2.83L38.59 0H40v1.41L1.41 40H0v-1.41zM40 38.59l-2.83-2.83L1.41 0H0v1.41L38.59 40H40v-1.41z'/%3E%3C/g%3E%3C/g%3E%3C/svg%3E");
            opacity: 0.5;
        }
        .cta-section .container {
            position: relative; /* Ensure content is above the pseudo-element */
        }

        /* --- Animation Keyframes & Scroll Animations --- */
        .fade-in-up {
            opacity: 0;
            transform: translateY(40px);
            transition: opacity 0.8s ease-out, transform 0.8s ease-out;
        }
        .fade-in-up.is-visible {
            opacity: 1;
            transform: translateY(0);
        }
        @keyframes fadeInDown { from { opacity: 0; transform: translateY(-30px); } to { opacity: 1; transform: translateY(0); } }
        @keyframes fadeInUp { from { opacity: 0; transform: translateY(30px); } to { opacity: 1; transform: translateY(0); } }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <!-- Hero Section -->
    <header class="hero-section text-center">
        <div class="container hero-content">
            <h1 class="display-4">Your Trusted Partner in Health</h1>
            <p class="lead">Delivering excellence and reliability in healthcare distribution with state-of-the-art technology and an unwavering commitment to wellness.</p>
            <div class="hero-buttons d-flex flex-column flex-sm-row justify-content-center gap-3">
                <a href="Customers.aspx" class="btn btn-primary btn-lg">
                    <i class="fas fa-pills me-2"></i>Explore Medications
                </a>
                <a href="Login.aspx" class="btn btn-outline-light btn-lg">
                    Get In Touch
                </a>
            </div>
        </div>
    </header>

    <!-- Features Section -->
    <section class="features-section">
        <div class="container">
            <div class="text-center section-title">
                <h2>Our Core Services</h2>
                <p class="lead text-muted">We provide a seamless ecosystem for all pharmaceutical needs.</p>
            </div>
            
            <div class="row g-4 text-center">
                <!-- Add 'fade-in-up' class for scroll animation -->
                <div class="col-md-6 col-lg-4 fade-in-up">
                    <div class="feature-card h-100">
                        <div class="feature-icon-wrapper"><i class="fas fa-capsules"></i></div>
                        <h3 class="card-title h5 fw-bold mb-3">Medication Management</h3>
                        <p class="card-text text-muted">Robust inventory control and stock management systems for all pharmaceutical products.</p>
                    </div>
                </div>
                
                <div class="col-md-6 col-lg-4 fade-in-up" style="transition-delay: 0.2s;">
                    <div class="feature-card h-100">
                        <div class="feature-icon-wrapper"><i class="fas fa-file-contract"></i></div>
                        <h3 class="card-title h5 fw-bold mb-3">Transparent Tenders</h3>
                        <p class="card-text text-muted">An open and efficient tender process for suppliers to bid on major pharmaceutical contracts.</p>
                    </div>
                </div>
                
                <div class="col-md-6 col-lg-4 fade-in-up" style="transition-delay: 0.4s;">
                    <div class="feature-card h-100">
                        <div class="feature-icon-wrapper"><i class="fas fa-truck-fast"></i></div>
                        <h3 class="card-title h5 fw-bold mb-3">Supply Chain Logistics</h3>
                        <p class="card-text text-muted">Streamlined ordering and distribution for pharmacies to request and receive medications.</p>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <!-- Why Choose Us Section -->
    <section class="why-us-section">
        <div class="container">
            <div class="row align-items-center g-5">
                <div class="col-lg-6 fade-in-up">
                    <img src="https://images.unsplash.com/photo-1576091160550-2173dba999ef?q=80&w=2670&auto=format&fit=crop" class="img-fluid" alt="Doctor with tablet" />
                </div>
                <div class="col-lg-6 fade-in-up" style="transition-delay: 0.2s;">
                    <div class="section-title text-start">
                        <h2 class="text-start">Why Choose SPC Pharmacy?</h2>
                    </div>
                    <p class="lead text-muted mb-4">We are more than a supplier; we are a partner in your success. Our commitment to quality, innovation, and service sets us apart.</p>
                    <ul class="list-unstyled why-us-list">
                        <li><i class="fas fa-check-circle"></i>Guaranteed Quality Assurance on all products.</li>
                        <li><i class="fas fa-check-circle"></i>Advanced technology for seamless ordering.</li>
                        <li><i class="fas fa-check-circle"></i>Reliable, on-time delivery network.</li>
                        <li><i class="fas fa-check-circle"></i>Dedicated support from industry experts.</li>
                    </ul>
                    <a href="About.aspx" class="btn btn-primary mt-3">Learn More About Us</a>
                </div>
            </div>
        </div>
    </section>

    <!-- Call to Action Section -->
    <section class="cta-section">
        <div class="container">
             <div class="row align-items-center text-center text-md-start">
                <div class="col-md-8">
                    <h2 class="fw-bold">Ready to Elevate Your Pharmaceutical Operations?</h2>
                    <p class="lead mb-0" style="opacity: 0.9;">Join our network of healthcare professionals and suppliers today.</p>
                </div>
                <div class="col-md-4 text-center text-md-end mt-4 mt-md-0">
                    <a href="Registation.aspx" class="btn btn-light btn-lg">
                        <i class="fas fa-user-plus me-2"></i>Register Now
                    </a>
                </div>
            </div>
        </div>
    </section>

    <!-- JavaScript for scroll animations -->
    <script>
        document.addEventListener("DOMContentLoaded", function () {
            const animatedElements = document.querySelectorAll('.fade-in-up');

            if ("IntersectionObserver" in window) {
                const observer = new IntersectionObserver((entries, observer) => {
                    entries.forEach(entry => {
                        if (entry.isIntersecting) {
                            entry.target.classList.add('is-visible');
                            observer.unobserve(entry.target);
                        }
                    });
                }, { threshold: 0.1 });

                animatedElements.forEach(el => {
                    observer.observe(el);
                });
            } else {
                // Fallback for older browsers
                animatedElements.forEach(el => {
                    el.classList.add('is-visible');
                });
            }
        });
    </script>
</asp:Content>
