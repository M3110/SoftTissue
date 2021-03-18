% Reduced Relaxation Function to Quasi-Linear Viscoelastic Fung Model
function y = Fung_SimplifiedReducedRelaxationFunction(time,mi0,mi1,mi2,mi3,tau1,tau2,tau3)

y = mi0 + mi1 * exp(-time / tau1) + mi2 * exp(-time / tau2) + mi3 * exp(-time / tau3);

end